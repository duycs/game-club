import { TestBed } from '@angular/core/testing';

import { GameClubService } from './game-club.service';

describe('GameClubService', () => {
  let service: GameClubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameClubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
