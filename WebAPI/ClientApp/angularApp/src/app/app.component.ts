import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {User} from "../types/User";
import {FormControl, FormGroup } from '@angular/forms';
import { MasterDataService } from './services/master-data.service';
import { UserService } from './services/user.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements  OnInit{
  title: string = 'angularApp';
  public users: User[] = []

  public userForm: FormGroup= new FormGroup({
    email: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
  });
  constructor(private userService: UserService, private masterData: MasterDataService) {}


  ngOnInit(): void {
    this.userService.getUsers().subscribe((data) => {
      this.users = data;
    });
    this.masterData.getIsAdminSelection().subscribe((data) => {console.log("getAdminSelection",data)});
    }

  addUser() {
    this.userService.addUser(this.userForm.value).subscribe((data) => {
      console.debug(data);
      this.userService.getUsers().subscribe((data) => {
        this.users = data;
      });
    });
  }

  emitDelete(user: User){
       this.userService.deleteUser(user.id).subscribe(data => {
        this.userService.getUsers().subscribe((data) => {
          this.users = data;
        });
      });

  }
}
