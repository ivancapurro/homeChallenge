import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Component } from '@angular/core';

@Component({
  template: `
    <ul class="list-group" *ngFor="let user of users">
      <li class="list-group-item">{{user.firstName}} {{user.lastName}}</li>
      <li class="list-group-item">{{user.email}}</li>
      <li class="list-group-item">Admin:  {{user.isAdmin}}</li>
      <button class="btn btn-danger delete-button" (click)="delete(user)">Delete</button>
    </ul>
  `
})
class TestComponent {
  users = [
    {
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      isAdmin: true
    },
    {
      firstName: 'Jane',
      lastName: 'Smith',
      email: 'jane@example.com',
      isAdmin: false
    }
  ];

  delete(user: any): void {
    // Delete logic goes here
  }
}

describe('TestComponent', () => {
  let component: TestComponent;
  let fixture: ComponentFixture<TestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TestComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should display user information correctly', () => {
    const userItems = fixture.debugElement.queryAll(By.css('.list-group-item'));
    const deleteButtons = fixture.debugElement.queryAll(By.css('.delete-button'));

    expect(userItems.length).toBe(6); // 3 list items for each user
    expect(deleteButtons.length).toBe(2); // 1 delete button for each user

    expect(userItems[0].nativeElement.textContent).toContain('John Doe');
    expect(userItems[1].nativeElement.textContent).toContain('john@example.com');
    expect(userItems[2].nativeElement.textContent).toContain('Admin: true');

    expect(userItems[3].nativeElement.textContent).toContain('Jane Smith');
    expect(userItems[4].nativeElement.textContent).toContain('jane@example.com');
    expect(userItems[5].nativeElement.textContent).toContain('Admin: false');
  });

  it('should call delete method when delete button is clicked', () => {
    spyOn(component, 'delete');

    const deleteButtons = fixture.debugElement.queryAll(By.css('.delete-button'));
    deleteButtons[0].nativeElement.click();

    expect(component.delete).toHaveBeenCalledWith(component.users[0]);
  });
});
