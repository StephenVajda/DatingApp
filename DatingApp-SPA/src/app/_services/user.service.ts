import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { idLocale } from 'ngx-bootstrap';
import { AlertifyService } from './alertify.service';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient,
    private alertify: AlertifyService) { }
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users');
  }
  getUser(id: any): Observable<User> {

    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  updateUser(id: number , user: User) {
    return this.http.put(this.baseUrl + 'users/' + id, user);
  }


}
