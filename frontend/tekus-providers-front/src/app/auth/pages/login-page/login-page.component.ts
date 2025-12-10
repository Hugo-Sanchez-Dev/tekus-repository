import { Component } from '@angular/core';
import { AuthLogin } from '../../components/auth-login.component';

@Component({
  selector: 'auth-login-page',
  standalone: true,
  imports: [AuthLogin],
  templateUrl: './login-page.component.html',
})
export class LoginPage { }
