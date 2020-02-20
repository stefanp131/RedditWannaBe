import { Component, OnInit } from '@angular/core';
import { Register } from 'src/models/Register';
import { AuthService } from 'src/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  username: string;
  password: string;
  confirmPassword: string;
  error: string;
  confirmError: string;

  constructor(
    private auth: AuthService,
    private router: Router,
  ) {}

  ngOnInit() {}

  register() {
    const registerModel: Register = {
      username: this.username,
      password: this.password
    };
    this.auth
      .register(registerModel).subscribe(
        () => this.router.navigate(['/login']),
        () => this.error = `'Password should be between 4-8 characters,
      or the username is already taken'`);
  }
}
