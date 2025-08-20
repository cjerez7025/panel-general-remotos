import { NgModule } from '@angular/core';
import { HighchartsChartModule } from 'highcharts-angular';

@NgModule({
  imports: [HighchartsChartModule],
  exports: [HighchartsChartModule]
})
export class HighchartsWrapperModule { }