import { HttpClient } from '@angular/common/http';
import {Inject, Injectable } from '@angular/core';
import { User } from 'src/types/User';
import { AppConfig } from 'src/types/AppConfig';
import { APP_CONFIG } from 'src/providers/config.provider';


@Injectable({
  providedIn: 'root'
})
export class UserService {


  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: AppConfig) {

  }


  getUsers() {
     return this.http.get<User[]>(`${this.config.apiEndpoint}/user/all`);
  }

  getUser(id: number) {
    return this.http.get('/users/' + id);
  }

  addUser(user: User) {
    return this.http.post(`${this.config.apiEndpoint}/user/add`, user);
  }

  updateUser(user: User) {
    return this.http.post('/users/' + user.id, user);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.config.apiEndpoint}/user/delete/${id}`);
  }

}
