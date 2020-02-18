import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/services/auth.service';
import { Router } from '@angular/router';
import { HeaderService } from 'src/services/header.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss', './header.component.mobile.scss']
})
export class HeaderComponent implements OnInit {
  name: string;

  constructor(private auth: AuthService, private router: Router, private header: HeaderService) { }

  ngOnInit() {
    this.header.currentMessage.subscribe(response => this.name = response);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);

    this.header.changeMessage('');
  }

  loggedIn() {
    return this.auth.loggedIn();
  }

}
