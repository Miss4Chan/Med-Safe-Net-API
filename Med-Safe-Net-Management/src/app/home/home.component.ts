import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterLinkActive,
    RouterOutlet,
    RouterLink
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
constructor(private router: Router, private accountService: AccountService) {}

  logout() {
    this.accountService.currentUser.set(null);
    localStorage.removeItem('user');
    this.router.navigate(['/login']);
  }
}
