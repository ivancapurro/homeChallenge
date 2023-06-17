import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserService } from './user.service';
import { APP_CONFIG } from 'src/providers/config.provider';
import { AppConfig } from 'src/types/AppConfig';
import { User } from 'src/types/User';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;
  const mockConfig: AppConfig = {
    apiEndpoint: 'https://localhost:7148/api'
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        UserService,
        { provide: APP_CONFIG, useValue: mockConfig }
      ]
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve the list of users from the server', () => {
    const mockUsers: User[] = [
      { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true },
      { id: 2, firstName: 'Jane', lastName: 'Smith', email: 'jane@example.com', isAdmin: false }
    ];

    service.getUsers().subscribe(users => {
      expect(users).toEqual(mockUsers);
    });

    const req = httpMock.expectOne(`${mockConfig.apiEndpoint}/user/all`);
    expect(req.request.method).toBe('GET');
    req.flush(mockUsers);
  });

  it('should retrieve a single user from the server', () => {
    const mockUser: User = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true };

    service.getUser(1).subscribe(user => {
      expect(user).toEqual(mockUser);
    });

    const req = httpMock.expectOne('/users/1');
    expect(req.request.method).toBe('GET');
    req.flush(mockUser);
  });

  it('should add a new user to the server', () => {
    const mockUser: User = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true };

    service.addUser(mockUser).subscribe(response => {
      expect(response).toEqual(mockUser);
    });

    const req = httpMock.expectOne(`${mockConfig.apiEndpoint}/user/add`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockUser);
    req.flush(mockUser);
  });

  it('should update an existing user on the server', () => {
    const mockUser: User = { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', isAdmin: true };

    service.updateUser(mockUser).subscribe(response => {
      expect(response).toEqual(mockUser);
    });

    const req = httpMock.expectOne('/users/1');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockUser);
    req.flush(mockUser);
  });

  it('should delete a user from the server', () => {
    const userId = 1;

    service.deleteUser(userId).subscribe();

    const req = httpMock.expectOne(`${mockConfig.apiEndpoint}/user/delete/${userId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });
});
