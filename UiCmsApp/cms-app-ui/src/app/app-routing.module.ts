import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainPageComponent} from '../app/components/main-page/main-page.component'
import {CustomerComponent} from './components/customer/customer.component'
import {LoginComponent}  from './components/login/login.component'
import {DashboardComponent} from './components/dashboard/dashboard/dashboard.component'

const routes: Routes = [

  { path: 'Customer', component: CustomerComponent },
  { path: 'login', component: LoginComponent },
  { path:'dashboard',component:DashboardComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
