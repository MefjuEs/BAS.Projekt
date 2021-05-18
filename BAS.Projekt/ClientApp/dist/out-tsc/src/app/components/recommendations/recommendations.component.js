import { __decorate } from "tslib";
import { Component, Input } from '@angular/core';
let RecommendationsComponent = class RecommendationsComponent {
    constructor() {
    }
    ngOnInit() {
        console.log(this.movieList);
    }
    getMoviePoster(poster) {
        if (poster != null) {
            return `data:${poster.contentType};base64,${poster.file}`;
        }
        else {
            return '/assets/images/noMovieImage.png';
        }
    }
};
__decorate([
    Input()
], RecommendationsComponent.prototype, "movieList", void 0);
RecommendationsComponent = __decorate([
    Component({
        selector: 'recommendations',
        templateUrl: './recommendations.component.html',
        styleUrls: ['./recommendations.component.css']
    })
], RecommendationsComponent);
export { RecommendationsComponent };
//# sourceMappingURL=recommendations.component.js.map