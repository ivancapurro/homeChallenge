import { Component, EventEmitter, Input, Output} from '@angular/core';
import { User } from 'src/types/User';


@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})

export class UsersListComponent  {
 @Input() users: User[] = [];
 @Output() emitDelete : EventEmitter<User> = new EventEmitter<User>();
  
  public delete(user: User) {
    this.emitDelete.emit(user);
  }
}
