import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {Genre} from "../../models/genre";
import {PlatformType} from "../../models/platformType";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {GameService} from "../../services/game.service";
import {Router} from "@angular/router";
import {EMPTY, mergeMap} from "rxjs";
import {Game} from "../../models/game";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
    selector: 'app-edit-game',
    templateUrl: './edit-game.component.html',
    styleUrls: ['./edit-game.component.scss']
})
export class EditGameComponent implements OnInit {
    @ViewChild('imageInput') imageInput;

    genres: Genre[] = [];
    platforms: PlatformType[] = [];
    form: FormGroup = new FormGroup({
        "key": new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
        "name": new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
        "description": new FormControl(null, [Validators.required, Validators.minLength(20), Validators.maxLength(500)]),
        "price": new FormControl(null, [Validators.required, Validators.pattern(/^\d*(?:[.,]\d{1,2})?$/)]),
        "genreIds": new FormControl(null),
        "platformIds": new FormControl(null),
    })

    constructor(private gameService: GameService,
                private router: Router,
                private dialogRef: MatDialogRef<EditGameComponent>,
                @Inject(MAT_DIALOG_DATA) private game: Game) {
    }

    ngOnInit(): void {
        this.gameService.getGenres().subscribe(result => this.genres = result);
        this.gameService.getPlatforms().subscribe(result => this.platforms = result);

        this.fillForm();
    }

    editGame() {
        this.gameService.editGame(this.game.key, this.form.getRawValue()).pipe(
            mergeMap((game) => {
                let image = this.imageInput.nativeElement.files[0];

                if(image) {
                    return this.gameService.setImage(this.game.key, image);
                }

                return EMPTY;
            })
        ).subscribe(r => {
            this.dialogRef.close();
            this.router.navigateByUrl(`/games/${this.form.value.key}`)
        })
    }

    fillForm() {
        this.form.patchValue({
            "key": this.game.key,
            "name": this.game.name,
            "description": this.game.description,
            "price": this.game.price,
            "genreIds": this.game?.genres.map(g => g.id),
            "platformIds": this.game.platformTypes?.map(pt => pt.id)
        });
    }
}
