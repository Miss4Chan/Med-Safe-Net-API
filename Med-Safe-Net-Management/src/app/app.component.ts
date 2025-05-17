import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiClient } from './services/apiClient';
import { lastValueFrom } from 'rxjs';
import { JsonPipe } from '@angular/common';

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
  constructor(private apiClient: ApiClient){
  }

  async ngOnInit(): Promise<void> {
    this.result = await lastValueFrom(this.apiClient.usersAll());
  }


}
