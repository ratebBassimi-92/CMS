import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http'
import { environment } from '../../../environments/environment-dev';

@Injectable({
  providedIn: 'root'
})
export class AuthLoginService {

  private baseUrl = environment.apiUrl;
  private tokenKey = 'authToken';
  constructor(
    private http:HttpClient
  ) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  loginUser(loginUserInput:any)
  {
      return this.http.post<any>(this.baseUrl+'/Login/LoginUser',loginUserInput)
  }

 // Save token to local storage
 saveToken(token: string): void {
  localStorage.setItem(this.tokenKey, token);
  console.log(localStorage)
}

// Get token from local storage
getToken(): string | null {
  return localStorage.getItem(this.tokenKey);
}

// Remove token from local storage
removeToken(): void {
  localStorage.removeItem(this.tokenKey);
}

}
