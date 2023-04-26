import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CSVInputComponent } from './csv-input.component';

describe('CSVInputComponent', () => {
  let component: CSVInputComponent;
  let fixture: ComponentFixture<CSVInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CSVInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CSVInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});