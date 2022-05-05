import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // two way binding is possible in angular
  // html -> ts / ts -> html is possible

  model: any = {};
  // sondaki $ işareti observable notationu 
  // account serviceyi public yapıp html dosyasından da bu serviceye erişebiliriz
  constructor(public accountService: AccountService) { }
  // injecting services to constructors here

  ngOnInit(): void {

  }

  login() {
    this.accountService.login(this.model).subscribe(response =>
      {
        console.log(response);
      }, error =>{
        console.log(error);
      })
  }

  logout() {
    this.accountService.logout();
  }

  // this is an antipattern, dont use it because we're getting the user directly
  // from account service
  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(user => {
  //       // double ! turn our object into boolean
  //       // null check olarak çalışıyor
  //       console.log(user);
  //     },error =>{
  //       console.log(error);
  //     })
  // }
}
