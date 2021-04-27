import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let NotificationService = class NotificationService {
    constructor(snackBar) {
        this.snackBar = snackBar;
    }
    showSnackBarNotification(message, buttonText, className) {
        this.snackBar.open(message, buttonText, {
            horizontalPosition: 'end',
            verticalPosition: 'top',
            duration: 10000,
            panelClass: [className]
        });
    }
};
NotificationService = __decorate([
    Injectable()
], NotificationService);
export { NotificationService };
//# sourceMappingURL=notification.service.js.map