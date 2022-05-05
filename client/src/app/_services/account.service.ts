import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';

// a service in angular follows the singleton principle
// they are injectable
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:5001/api/"; 
  // use = for setting the property
  // type definitionu için : kullanıyoruz

  // were gonna create an observable to store our users
  private currentUserSource = new ReplaySubject<User>(1); // kinda like a buffer
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any){ // sending post request here
    return this.http.post(this.baseUrl + "account/login",model).pipe(
      map((response: User) => {
        const user = response;
        if (user){
          localStorage.setItem("user",JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model: any){
    return this.http.post(this.baseUrl + "account/register", model).pipe(
      map((user: User) =>{
        if (user){
          localStorage.setItem("user",JSON.stringify(user));
          this.currentUserSource.next(user);  
        }
        //return user; // map fonksiyonu içinde dönen şeye ihtiyacın varsa böyle dönüyorsun
      }))
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }


  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }
}
