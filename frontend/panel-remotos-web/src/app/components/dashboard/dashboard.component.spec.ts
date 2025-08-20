import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService, QuickStats, SponsorStats } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  // Propiedades principales
  quickStats: QuickStats | null = null;
  syncStatus: any[] = [];
  loading = true;
  error: string | null = null;
  showWarnings = false;
  activeTab = 'llamadas';

  // Propiedades para drill-down y navegación
  currentView = 'main'; // 'main' | 'detail'
  selectedSponsor: any = null;
  executiveData: any[] = [];

  // Utilidades
  Math = Math;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loadDashboardData();
  }

  // ===========================
  // MÉTODOS DE CARGA DE DATOS
  // ===========================

  loadDashboardData(): void {
    this.loading = true;
    this.error = null;

    // Cargar estadísticas rápidas
    this.dashboardService.getQuickStats().subscribe({
      next: (data) => {
        this.quickStats = this.processQuickStats(data);
        this.loading = false;
        this.detectWarnings();
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
        this.detectWarnings();
      },
      error: (err) => {
        console.error('Error loading sync status:', err);
      }
    });
  }

  private processQuickStats(data: QuickStats): any {
    // Convertir SponsorStats a formato extendido para el template
    const processedData = {
      ...data,
      sponsors: data.sponsorBreakdown?.map(sponsor => this.processSponsor(sponsor)) || [],
      totals: this.calculateTotals(data)
    };

    return processedData;
  }

  private processSponsor(sponsor: SponsorStats): any {
    // Convertir SponsorStats a formato que espera el template
    const processed: any = {
      id: sponsor.sponsorName,
      name: sponsor.sponsorName,
      totalLeads: Math.floor(sponsor.callsToday * 1.5), // Estimación
      totalCalls: sponsor.callsToday,
      callsToday: sponsor.callsToday,
      dailyGoal: sponsor.dailyGoal,
      goalPercentage: sponsor.goalPercentage,
      status: sponsor.status,
      colorHex: sponsor.colorHex,
      activeExecutives: sponsor.activeExecutives,
      
      // Propiedades calculadas
      connectedCalls: Math.floor(sponsor.callsToday * 0.7), // 70% estimado
      sales: Math.floor(sponsor.callsToday * 0.1), // 10% estimado
      notConnectedCalls: 0,
      
      // Propiedades para drill-down
      hasIssues: sponsor.goalPercentage < 70 || sponsor.status >= 2,
      issueCount: this.getSponsorIssueCount(sponsor),
      severity: this.getSponsorSeverity(sponsor),
      expanded: false,
      executives: [],
      
      // Propiedades adicionales
      contactabilityRate: 0,
      effectivenessRate: 0,
      conversionRate: 0,
      notConnectedRate: 0,
      targetProgress: 0,
      performanceScore: 0,
      lostCalls: 0
    };

    // Calcular porcentajes
    processed.notConnectedCalls = processed.totalCalls - processed.connectedCalls;
    processed.contactabilityRate = processed.totalLeads > 0 ? processed.totalCalls / processed.totalLeads : 0;
    processed.effectivenessRate = processed.totalCalls > 0 ? processed.connectedCalls / processed.totalCalls : 0;
    processed.conversionRate = processed.connectedCalls > 0 ? processed.sales / processed.connectedCalls : 0;
    processed.notConnectedRate = processed.totalCalls > 0 ? processed.notConnectedCalls / processed.totalCalls : 0;
    processed.targetProgress = processed.dailyGoal > 0 ? processed.totalCalls / processed.dailyGoal : 0;
    processed.performanceScore = (processed.effectivenessRate + processed.conversionRate) / 2;
    processed.lostCalls = Math.floor(processed.totalCalls * 0.05); // 5% estimado

    return processed;
  }

  private getSponsorIssueCount(sponsor: SponsorStats): number {
    let count = 0;
    if (sponsor.goalPercentage < 70) count++;
    if (sponsor.status >= 2) count++;
    if (sponsor.activeExecutives === 0) count++;
    return count;
  }

  private getSponsorSeverity(sponsor: SponsorStats): string {
    if (sponsor.status >= 3 || sponsor.goalPercentage < 40) return 'error';
    if (sponsor.status >= 2 || sponsor.goalPercentage < 70) return 'warning';
    return 'normal';
  }

  private calculateTotals(data: QuickStats): any {
    if (!data.sponsorBreakdown?.length) return null;

    const totals: any = data.sponsorBreakdown.reduce((acc: any, sponsor) => {
      acc.totalLeads += Math.floor(sponsor.callsToday * 1.5);
      acc.totalCalls += sponsor.callsToday;
      acc.connectedCalls += Math.floor(sponsor.callsToday * 0.7);
      acc.sales += Math.floor(sponsor.callsToday * 0.1);
      acc.totalTarget += sponsor.dailyGoal;
      return acc;
    }, {
      totalLeads: 0,
      totalCalls: 0,
      connectedCalls: 0,
      sales: 0,
      totalTarget: 0,
      contactabilityRate: 0,
      effectivenessRate: 0,
      conversionRate: 0,
      notConnectedCalls: 0,
      notConnectedRate: 0,
      overallTargetProgress: 0,
      overallPerformance: 0,
      totalLostCalls: 0
    });

    // Calcular porcentajes
    totals.contactabilityRate = totals.totalCalls > 0 ? totals.totalCalls / totals.totalLeads : 0;
    totals.effectivenessRate = totals.totalCalls > 0 ? totals.connectedCalls / totals.totalCalls : 0;
    totals.conversionRate = totals.connectedCalls > 0 ? totals.sales / totals.connectedCalls : 0;
    totals.notConnectedCalls = totals.totalCalls - totals.connectedCalls;
    totals.notConnectedRate = totals.totalCalls > 0 ? totals.notConnectedCalls / totals.totalCalls : 0;
    totals.overallTargetProgress = totals.totalTarget > 0 ? totals.totalCalls / totals.totalTarget : 0;
    totals.overallPerformance = (totals.effectivenessRate + totals.conversionRate) / 2;
    totals.totalLostCalls = Math.floor(totals.totalCalls * 0.05);

    return totals;
  }

  private detectWarnings(): void {
    this.showWarnings = !!(
      this.quickStats?.hasSyncIssues ||
      this.error ||
      (this.quickStats?.problematicSponsors || 0) > 0
    );
  }

  // ===========================
  // MÉTODOS DE NAVEGACIÓN
  // ===========================

  setActiveTab(tab: string): void {
    this.activeTab = tab;
    // Reset drill-down al cambiar de tab
    this.currentView = 'main';
    this.selectedSponsor = null;
  }

  viewSponsorDetail(sponsor: any): void {
    this.selectedSponsor = sponsor;
    this.currentView = 'detail';
    this.loadExecutiveData(sponsor);
  }

  backToMain(): void {
    this.currentView = 'main';
    this.selectedSponsor = null;
    this.executiveData = [];
  }

  toggleSponsorExpansion(sponsor: any): void {
    sponsor.expanded = !sponsor.expanded;
    if (sponsor.expanded && (!sponsor.executives || sponsor.executives.length === 0)) {
      sponsor.executives = this.generateMockExecutiveData(sponsor);
    }
  }

  private loadExecutiveData(sponsor: any): void {
    // Generar datos de ejecutivos para la vista de detalle
    this.executiveData = this.generateMockExecutiveData(sponsor);
  }

  private generateMockExecutiveData(sponsor: any): any[] {
    // Datos mock para desarrollo - reemplazar con service real
    const executiveNames = ['JUAN PÉREZ', 'MARÍA GARCÍA', 'CARLOS RODRIGUEZ', 'ANA MARTÍNEZ'];
    const executivesCount = Math.min(executiveNames.length, sponsor.activeExecutives || 2);
    
    return Array.from({ length: executivesCount }, (_, index) => {
      const totalCalls = Math.floor((sponsor.totalCalls || 50) / executivesCount) + Math.floor(Math.random() * 10);
      const connectedCalls = Math.floor(totalCalls * (0.6 + Math.random() * 0.2)); // 60-80%
      const totalLeads = Math.floor(totalCalls * (1.3 + Math.random() * 0.4)); // 130-170%
      
      return {
        id: index + 1,
        name: executiveNames[index] || `EJECUTIVO ${index + 1}`,
        totalLeads: totalLeads,
        totalCalls: totalCalls,
        connectedCalls: connectedCalls,
        sales: Math.floor(connectedCalls * (0.05 + Math.random() * 0.15)), // 5-20%
        target: Math.floor((sponsor.dailyGoal || 100) / executivesCount),
        contactabilityRate: totalLeads > 0 ? totalCalls / totalLeads : 0,
        effectivenessRate: totalCalls > 0 ? connectedCalls / totalCalls : 0,
        conversionRate: connectedCalls > 0 ? Math.floor(connectedCalls * 0.1) / connectedCalls : 0,
        notConnectedCalls: totalCalls - connectedCalls,
        notConnectedRate: totalCalls > 0 ? (totalCalls - connectedCalls) / totalCalls : 0,
        targetProgress: sponsor.dailyGoal > 0 ? totalCalls / (sponsor.dailyGoal / executivesCount) : 0,
        performanceScore: Math.random() * 0.4 + 0.4, // 40-80%
        lostCalls: Math.floor(Math.random() * 5)
      };
    });
  }

  // ===========================
  // MÉTODOS DE ACCIONES
  // ===========================

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
        this.error = 'Error durante la actualización: ' + err.message;
        this.loading = false;
      }
    });
  }

  // ===========================
  // MÉTODOS DE ESTILOS Y CLASES CSS
  // ===========================

  getSponsorRowClass(sponsor: any): string {
    const classes = [];
    
    if (sponsor.hasIssues) {
      classes.push(sponsor.severity === 'error' ? 'row-error' : 'row-warning');
    }
    
    // Clase para estado de rendimiento
    const goalPercentage = sponsor.goalPercentage || 0;
    if (goalPercentage >= 80) {
      classes.push('row-excellent');
    } else if (goalPercentage >= 60) {
      classes.push('row-good');
    } else if (goalPercentage >= 40) {
      classes.push('row-regular');
    } else {
      classes.push('row-poor');
    }

    return classes.join(' ');
  }

  getPercentageClass(value: number, inverse: boolean = false): string {
    if (value == null || isNaN(value)) return 'percentage-unknown';
    
    const percentage = typeof value === 'number' ? (value > 1 ? value : value * 100) : 0;
    
    if (inverse) {
      // Para porcentajes donde menor es mejor (ej: % no conectadas)
      if (percentage <= 20) return 'percentage excellent';
      if (percentage <= 40) return 'percentage good';
      if (percentage <= 60) return 'percentage regular';
      return 'percentage poor';
    } else {
      // Para porcentajes donde mayor es mejor
      if (percentage >= 80) return 'percentage excellent';
      if (percentage >= 60) return 'percentage good';
      if (percentage >= 40) return 'percentage regular';
      return 'percentage poor';
    }
  }

  getRendimientoClass(value: number): string {
    if (value == null || isNaN(value)) return 'rendimiento unknown';
    
    const percentage = typeof value === 'number' ? (value > 1 ? value : value * 100) : 0;
    
    if (percentage >= 80) return 'rendimiento excellent';
    if (percentage >= 60) return 'rendimiento good';
    if (percentage >= 40) return 'rendimiento regular';
    return 'rendimiento poor';
  }

  getStatusColor(sponsor: any): string {
    const status = sponsor.status || 0;
    switch (status) {
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

  // ===========================
  // MÉTODOS UTILITARIOS
  // ===========================

  formatPercentage(value: number): string {
    if (value == null || isNaN(value)) return '0%';
    const percentage = value > 1 ? value : value * 100;
    return `${percentage.toFixed(1)}%`;
  }

  formatNumber(value: number): string {
    if (value == null || isNaN(value)) return '0';
    return value.toLocaleString('es-ES');
  }

  isDataStale(): boolean {
    if (!this.quickStats?.lastUpdateTimestamp) return true;
    
    const lastUpdate = new Date(this.quickStats.lastUpdateTimestamp);
    const now = new Date();
    const diffMinutes = (now.getTime() - lastUpdate.getTime()) / (1000 * 60);
    
    return diffMinutes > 30; // Considera datos obsoletos después de 30 minutos
  }

  hasActiveWarnings(): boolean {
    return this.showWarnings || this.isDataStale();
  }
}