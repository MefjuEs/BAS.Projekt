import { TestBed } from '@angular/core/testing';
import { UserService } from './user.service';
describe('RoleService', () => {
    let service;
    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(UserService);
    });
    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=user.service.spec.js.map