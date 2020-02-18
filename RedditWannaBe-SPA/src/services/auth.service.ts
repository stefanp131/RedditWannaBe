import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Register } from 'src/models/Register';
import { Login } from 'src/models/Login';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginResponse } from 'src/models/LoginResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:44366/api/auth';
  constructor(private httpClient: HttpClient) { }

  register(model: Register) {
    return this.httpClient.post(this.baseUrl + '/register', model);
  }

  login(model: Login): Observable<HttpResponse<LoginResponse>> {
    return this.httpClient.post<LoginResponse>(this.baseUrl + '/login', model, {observe: 'response'});
  }

  logout() {
    localStorage.removeItem('userInfo');
    localStorage.removeItem('token');
  }

  loggedIn() {
    const helper = new JwtHelperService();
    const token = localStorage.getItem('token');

    return !helper.isTokenExpired(token);
  }
}
