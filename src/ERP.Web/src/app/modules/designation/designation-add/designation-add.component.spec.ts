import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignationAddComponent } from './designation-add.component';

describe('DesignationAddComponent', () => {
  let component: DesignationAddComponent;
  let fixture: ComponentFixture<DesignationAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DesignationAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignationAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
