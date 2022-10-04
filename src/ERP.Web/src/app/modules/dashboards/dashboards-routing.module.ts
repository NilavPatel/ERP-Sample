import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneralDashboardComponent } from './general-dashboard/general-dashboard.component';

const routes: Routes = [{
  path: '',
  component: GeneralDashboardComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardsRoutingModule { }
