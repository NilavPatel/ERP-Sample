import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignationEditComponent } from './designation-edit.component';

describe('DesignationEditComponent', () => {
  let component: DesignationEditComponent;
  let fixture: ComponentFixture<DesignationEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DesignationEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
