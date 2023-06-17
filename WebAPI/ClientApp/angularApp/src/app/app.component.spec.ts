import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserService } from './services/user.service';
import { MasterDataService } from './services/master-data.service';
import { of } from 'rxjs';
import { User } from '../types/User';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let userService: UserService;
  let masterDataService: MasterDataService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule],
      providers: [UserService, MasterDataService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    userService = TestBed.inject(UserService);
    masterDataService = TestBed.inject(MasterDataService);
    fixture.detectChanges();
  });

  it('should create the app component', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve users on component initialization', () => {
    const mockUsers: User[] = [
      { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true },
      { id: 2, firstName: 'Jane', lastName: 'Smith', email: 'jane@example.com', isAdmin: false },
    ];
    spyOn(userService, 'getUsers').and.returnValue(of(mockUsers));

    component.ngOnInit();

    expect(userService.getUsers).toHaveBeenCalled();
    expect(component.users).toEqual(mockUsers);
  });

  it('should add a user and retrieve updated users on addUser()', () => {
    const mockUser: User = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true };
    const mockUsers: User[] = [mockUser];
    spyOn(userService, 'addUser').and.returnValue(of(mockUser));
    spyOn(userService, 'getUsers').and.returnValue(of(mockUsers));

    component.userForm.patchValue(mockUser);
    component.addUser();

    expect(userService.addUser).toHaveBeenCalledWith(mockUser);
    expect(userService.getUsers).toHaveBeenCalled();
    expect(component.users).toEqual(mockUsers);
  });

  it('should delete a user and retrieve updated users on emitDelete()', () => {
    const mockUser: User = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true };
    const mockUsers: User[] = [];
    spyOn(userService, 'deleteUser').and.returnValue(of({}));
    spyOn(userService, 'getUsers').and.returnValue(of(mockUsers));

    component.emitDelete(mockUser);

    expect(userService.deleteUser).toHaveBeenCalledWith(mockUser.id);
    expect(userService.getUsers).toHaveBeenCalled();
    expect(component.users).toEqual(mockUsers);
  });
});
