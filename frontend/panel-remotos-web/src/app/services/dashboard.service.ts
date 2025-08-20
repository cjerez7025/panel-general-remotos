import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface QuickStats {
  totalCallsToday: number;
  callsChangePercentage: number;
  activeSponsors: number;
  problematicSponsors: number;
  contactedPercentage: number;
  goalProgressPercentage: number;
  totalDailyGoal: number;
  totalActiveExecutives: number;
  averageCallsPerExecutive: number;
  lastUpdateTimestamp: string;
  hasSyncIssues: boolean;
  minutesSinceLastSync: number;
  systemStatus: number;
  statusMessage: string;
  sponsorBreakdown: SponsorStats[];
  trendIndicator: string;
}

export interface SponsorStats {
  sponsorName: string;
  callsToday: number;
  dailyGoal: number;
  goalPercentage: number;
  status: number;
  colorHex: string;
  activeExecutives: number;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = 'http://localhost:5000/api/dashboard';

  constructor(private http: HttpClient) { }

  getQuickStats(): Observable<QuickStats> {
    return this.http.get<QuickStats>(`${this.apiUrl}/quick-stats`);
  }

  getSyncStatus(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/sync-status`);
  }

  refreshDashboard(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/refresh`, {});
  }

  getSystemAlerts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/alerts`);
  }
}