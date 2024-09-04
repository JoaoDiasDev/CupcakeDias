import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CupcakeListComponent } from './cupcake-list.component';

describe('CupcakeListComponent', () => {
  let component: CupcakeListComponent;
  let fixture: ComponentFixture<CupcakeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CupcakeListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CupcakeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
