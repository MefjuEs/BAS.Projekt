import { __decorate } from "tslib";
import { UserRole } from './../../interfaces/auth/role';
import { Component } from '@angular/core';
let NavbarComponent = class NavbarComponent {
    constructor(router, authService) {
        this.router = router;
        this.authService = authService;
        this.authService.currentUser.subscribe(x => this.currentUser = x);
    }
    ngOnInit() {
    }
    isAdmin() {
        return this.currentUser && this.currentUser.userRole === UserRole.Admin;
    }
    logout() {
        this.authService.logout();
        location.reload();
        //this.router.navigate(['/']);
    }
};
NavbarComponent = __decorate([
    Component({
        selector: 'navbar',
        templateUrl: './navbar.component.html',
        styleUrls: ['./navbar.component.css']
    })
], NavbarComponent);
export { NavbarComponent };
//# sourceMappingURL=navbar.component.js.map