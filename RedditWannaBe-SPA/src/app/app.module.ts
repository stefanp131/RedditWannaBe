import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from '../components/home/home.component';
import { FooterComponent } from '../components/footer/footer.component';
import { HeaderComponent } from '../components/header/header.component';
import { NotFoundComponent } from '../components/not-found/not-found.component';
import { LoginComponent } from '../components/login/login.component';
import { JwtInterceptorService } from 'src/services/jwtInterceptor.service';
import { RegisterComponent } from 'src/components/register/register.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      FooterComponent,
      HeaderComponent,
      NotFoundComponent,
      LoginComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      AppRoutingModule
   ],
   providers: [{
      provide: HTTP_INTERCEPTORS,
        useClass: JwtInterceptorService,
        multi: true
    }],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
