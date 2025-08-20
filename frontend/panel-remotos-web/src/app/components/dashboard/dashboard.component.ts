import { Component, OnInit } from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { HighchartsWrapperModule } from '../../shared/highcharts.module';
import * as Highcharts from 'highcharts';
import { DashboardService, QuickStats } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, DecimalPipe, HighchartsWrapperModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  quickStats: QuickStats | null = null;
  syncStatus: any[] = [];
  loading = true;
  error: string | null = null;
  showWarnings = false;
  activeTab = 'llamadas';

  // Highcharts
  Highcharts: typeof Highcharts = Highcharts;
  callsChartOptions: Highcharts.Options = {};
  progressChartOptions: Highcharts.Options = {};
  performanceChartOptions: Highcharts.Options = {};

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loadDashboardData();
    this.initializeCharts();
  }

  loadDashboardData(): void {
    this.loading = true;
    this.error = null;

    this.dashboardService.getQuickStats().subscribe({
      next: (data) => {
        this.quickStats = data;
        this.loading = false;
        this.updateCharts();
      },
      error: (err) => {
        this.error = 'Error al cargar los datos: ' + err.message;
        this.loading = false;
        console.error('Error loading dashboard data:', err);
      }
    });

    this.dashboardService.getSyncStatus().subscribe({
      next: (data) => {
        this.syncStatus = data;
      },
      error: (err) => {
        console.error('Error loading sync status:', err);
      }
    });
  }

  initializeCharts(): void {
    // Gráfico de llamadas por sponsor
    this.callsChartOptions = {
      chart: {
        type: 'column',
        backgroundColor: 'transparent',
        height: 300
      },
      title: {
        text: 'Llamadas por Sponsor',
        style: {
          fontSize: '16px',
          fontWeight: 'bold',
          color: '#374151'
        }
      },
      xAxis: {
        categories: ['ACHS', 'INTERCLINICA', 'BANMEDICA'],
        labels: {
          style: {
            color: '#6B7280'
          }
        }
      },
      yAxis: {
        title: {
          text: 'Llamadas',
          style: {
            color: '#6B7280'
          }
        },
        labels: {
          style: {
            color: '#6B7280'
          }
        }
      },
      plotOptions: {
        column: {
          borderRadius: 4,
          colorByPoint: true,
          colors: ['#10B981', '#3B82F6', '#F59E0B']
        }
      },
      series: [{
        name: 'Llamadas',
        type: 'column',
        data: [350, 280, 217]
      }],
      legend: {
        enabled: false
      },
      credits: {
        enabled: false
      }
    };

    // Gráfico de progreso circular
    this.progressChartOptions = {
      chart: {
        type: 'pie',
        backgroundColor: 'transparent',
        height: 250
      },
      title: {
        text: 'Progreso de Metas',
        style: {
          fontSize: '16px',
          fontWeight: 'bold',
          color: '#374151'
        }
      },
      plotOptions: {
        pie: {
          innerSize: '60%',
          dataLabels: {
            enabled: true,
            format: '{point.name}: {point.percentage:.1f}%',
            style: {
              fontSize: '12px',
              color: '#374151'
            }
          },
          colors: ['#10B981', '#3B82F6', '#F59E0B']
        }
      },
      series: [{
        name: 'Progreso',
        type: 'pie',
        data: [
          { name: 'ACHS', y: 87.5 },
          { name: 'INTERCLINICA', y: 80.0 },
          { name: 'BANMEDICA', y: 86.8 }
        ]
      }],
      credits: {
        enabled: false
      }
    };

    // Gráfico de rendimiento
    this.performanceChartOptions = {
      chart: {
        type: 'spline',
        backgroundColor: 'transparent',
        height: 300
      },
      title: {
        text: 'Tendencia de Llamadas (Últimos 7 días)',
        style: {
          fontSize: '16px',
          fontWeight: 'bold',
          color: '#374151'
        }
      },
      xAxis: {
        categories: ['Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb', 'Dom'],
        labels: {
          style: {
            color: '#6B7280'
          }
        }
      },
      yAxis: {
        title: {
          text: 'Llamadas',
          style: {
            color: '#6B7280'
          }
        },
        labels: {
          style: {
            color: '#6B7280'
          }
        }
      },
      plotOptions: {
        spline: {
          lineWidth: 3,
          marker: {
            radius: 5
          }
        }
      },
      series: [{
        name: 'ACHS',
        type: 'spline',
        data: [320, 340, 350, 380, 370, 360, 350],
        color: '#10B981'
      }, {
        name: 'INTERCLINICA',
        type: 'spline',
        data: [250, 270, 280, 290, 285, 275, 280],
        color: '#3B82F6'
      }, {
        name: 'BANMEDICA',
        type: 'spline',
        data: [200, 210, 217, 225, 220, 215, 217],
        color: '#F59E0B'
      }],
      legend: {
        align: 'center',
        verticalAlign: 'bottom',
        itemStyle: {
          color: '#374151'
        }
      },
      credits: {
        enabled: false
      }
    };
  }

  updateCharts(): void {
    if (this.quickStats && this.quickStats.sponsorBreakdown) {
      const sponsors = this.quickStats.sponsorBreakdown;
      console.log('Updating charts with sponsor data:', sponsors);
    }
  }

  refreshData(): void {
    this.loadDashboardData();
  }

  forceRefresh(): void {
    this.loading = true;
    this.dashboardService.refreshDashboard().subscribe({
      next: (result) => {
        console.log('Refresh result:', result);
        setTimeout(() => {
          this.loadDashboardData();
        }, 2000);
      },
      error: (err) => {
        console.error('Error during refresh:', err);
        this.loading = false;
      }
    });
  }

  toggleWarnings(): void {
    this.showWarnings = !this.showWarnings;
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

  drillDownSponsor(sponsor: any): void {
    console.log('Drill down sponsor:', sponsor);
  }

  getStatusColor(sponsor: any): string {
    if (!sponsor || sponsor.status === undefined) return '#6B7280';
    
    switch (sponsor.status) {
      case 0: return '#10B981';
      case 1: return '#3B82F6';
      case 2: return '#F59E0B';
      case 3: return '#EF4444';
      default: return '#6B7280';
    }
  }

  getSystemStatusText(status: number): string {
    switch (status) {
      case 0: return 'Saludable';
      case 1: return 'Advertencia';
      case 2: return 'Crítico';
      case 3: return 'Inactivo';
      default: return 'Desconocido';
    }
  }
}