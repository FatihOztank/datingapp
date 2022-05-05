import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './_services/account.service';
// angularda düz bootstrap kullanma ngx bootstrap var onu kullan
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Dating App';
  users: any;
  constructor(private accountService: AccountService){}

  ngOnInit() {
    //this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem("user"));
    this.accountService.setCurrentUser(user);
  }

  // we're not gonna use this anymore
  // getUsers(){ //thats how u get data from observable
  //   this.http.get("https://localhost:5001/api/users")
  //   .subscribe(response =>{
  //     this.users = response;
  //   }, error =>{
  //     console.log(error);
  //   })
  // }
}
