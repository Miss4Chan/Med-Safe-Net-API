import { Injectable, signal } from '@angular/core';
import { UserDto } from './apiClient';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  currentUser = signal<UserDto>(null);
  constructor() { }
}
