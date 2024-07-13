import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http'
import { environment } from '../../../environments/environment-dev';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private baseUrl = environment.apiUrl;
  constructor(
    private http:HttpClient
  ) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  saveCustomer(customerData:any)
  {

      return this.http.post<any>(this.baseUrl+'/Customer/AddCustomer',customerData)
  }

  getCustomerList()
    {
      return this.http.get<any>(this.baseUrl+'/Customer/GetListCustomer')
    }

}
