export interface GameCreate {
    key: string;
    name: string;
    description: string;
    price: number;
    genreIds: number[];
    platformIds: number[];
}