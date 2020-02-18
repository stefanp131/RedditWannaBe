import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Login } from 'src/models/Login';
import { Router } from '@angular/router';
import { HeaderService } from 'src/services/header.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;
  error: string;

  constructor(private auth: AuthService, private router: Router, private header: HeaderService) {}

  ngOnInit() {}

  login() {
    const helper = new JwtHelperService();
    const loginModel: Login = {
      username: this.username,
      password: this.password
    };
    this.auth.login(loginModel).subscribe(response => {

      if(response.status === 401){
        this.error = 'Unauthorized';
        return;
      }

      this.error = '';
      localStorage.setItem('token', response.body.token);
      const decodedToken = helper.decodeToken(response.body.token);
      localStorage.setItem('userInfo', JSON.stringify(decodedToken));
      this.header.changeMessage(decodedToken.unique_name);
      this.router.navigate(['/home']);
    });
  }
}
