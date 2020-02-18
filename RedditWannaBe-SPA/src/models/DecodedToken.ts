export interface DecodedToken {
    nameid: string;
    unique_name: string;
    nbf: number;
    exp: number;
    iat: number;
}