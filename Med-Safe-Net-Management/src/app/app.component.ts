import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { JsonPipe } from '@angular/common';
import { AccountService } from './services/account.service';
import { UserDto } from './services/apiClient';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    JsonPipe
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  result:any = "Does not work";
  constructor(private accountService: AccountService){
  }

  async ngOnInit(): Promise<void> {
    const user = localStorage.getItem('user');
    if(user){
      const userDto = new UserDto(JSON.parse(user));
      this.accountService.currentUser.set(userDto);
    }
  }


}
