import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe, DecimalPipe } from '@angular/common';
import { DashboardService, QuickStats } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, DatePipe, DecimalPipe],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  quickStats: QuickStats | null = null;
  syncStatus: any[] = [];
  loading = true;
  error: string | null = null;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;
    this.error = null;

    // Cargar estadísticas rápidas
    this.dashboardService.getQuickStats().subscribe({
      next: (data) => {
        this.quickStats = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar los datos: ' + err.message;
        this.loading = false;
        console.error('Error loading dashboard data:', err);
      }
    });

    // Cargar estado de sincronización
    this.dashboardService.getSyncStatus().subscribe({
      next: (data) => {
        this.syncStatus = data;
      },
      error: (err) => {
        console.error('Error loading sync status:', err);
      }
    });
  }

  refreshData(): void {
    this.loadDashboardData();
  }

  forceRefresh(): void {
    this.loading = true;
    this.dashboardService.refreshDashboard().subscribe({
      next: (result) => {
        console.log('Refresh result:', result);
        // Esperar un momento y recargar datos
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

  getStatusColor(sponsor: any): string {
    switch (sponsor.status) {
      case 0: return '#10B981'; // Verde - Excelente
      case 1: return '#3B82F6'; // Azul - Bueno
      case 2: return '#F59E0B'; // Amarillo - Regular
      case 3: return '#EF4444'; // Rojo - Pobre
      default: return '#6B7280'; // Gris - Sin datos
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