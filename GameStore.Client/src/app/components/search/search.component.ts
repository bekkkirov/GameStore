import {Component, OnInit} from '@angular/core';
import {Genre} from "../../models/genre";
import {GameService} from "../../services/game.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {take} from "rxjs";

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
    genres: Genre[];
    form: FormGroup = new FormGroup({
       "namePattern": new FormControl(null, [Validators.minLength(3), Validators.pattern(".*\\S.*[a-zA-z0-9]")]),
       "genreIds": new FormControl(null),
    });

    constructor(private gameService: GameService,
                private route: ActivatedRoute,
                private router: Router) {
    }

    ngOnInit(): void {
        this.setFilters();
        this.getGenres();
    }

    search() {
       this.router.navigate([''], { queryParams: this.form.getRawValue() });
    }

    reset() {
        this.form.reset();
        this.router.navigateByUrl('/');
    }

    setFilters() {
        this.route.queryParamMap.pipe(take(1)).subscribe(params => {
            this.form.patchValue({
                "namePattern": params.get('namePattern') ?? "",
                "genreIds": params.getAll('genreIds').map(id => +id),
            })
        });
    }

    getGenres() {
        this.gameService.getGenres().subscribe(result => this.genres = result);
    }
}
