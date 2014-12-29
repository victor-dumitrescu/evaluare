﻿module Protein

let (female: List<List<int>>) = 
              [[54; 56; 56; 59; 61; 61; 62; 63; 66; 66; 67; 69; 71; 72; 74; 74; 76; 77; 80; 80; 82; 83; 85; 86; 86; 88; 89]; 
               [66; 70; 72; 73; 74; 76; 77; 78; 81; 82; 84; 84; 87; 89; 91; 93; 95; 96; 97; 99; 102; 103; 105; 106; 108; 109; 111]; 
               [80; 82; 85; 85; 88; 89; 92; 94; 97; 97; 98; 102; 103; 105; 106; 109; 110; 113; 115; 117; 118; 120; 122; 125; 126; 129; 130]; 
               [93; 95; 97; 100; 102; 104; 106; 108; 110; 113; 115; 117; 119; 121; 124; 126; 128; 129; 131; 133; 136; 138; 141; 143; 146; 148; 150]]

let (male: List<List<int>>) =
              [[82; 84; 86; 87; 89; 92; 92; 93; 95; 97; 98; 99; 102; 104; 105; 107; 109; 110; 111; 114; 116; 117; 119; 120; 122; 125; 127];
               [97; 98; 99; 102; 104; 106; 107; 110; 111; 114; 115; 118; 119; 120; 122; 125; 127; 131; 131; 132; 135; 136; 139; 140; 141; 143; 146];
               [110; 113; 115; 118; 119; 122; 125; 127; 129; 131; 132; 135; 137; 139; 141; 143; 146; 148; 150; 152; 153; 155; 159; 161; 163; 165; 168];
               [126; 130; 132; 133; 137; 140; 141; 143; 147; 149; 151; 154; 157; 159; 162; 162; 165; 169; 171; 173; 176; 177; 180; 183; 185; 187; 191]]

let computeProteins (imc: int, height: int, sex: int) =
    match sex with
    | 0 -> match height with
           | h when h >= 147 && h <= 153 -> female.[0].[imc-19] |> Some
           | h when h >= 154 && h <= 163 -> female.[1].[imc-19] |> Some
           | h when h >= 164 && h <= 173 -> female.[2].[imc-19] |> Some
           | h when h >= 174 && h <= 183 -> female.[3].[imc-19] |> Some
           | _ -> None
    | 1 -> match height with
           | h when h >= 154 && h <= 163 -> male.[0].[imc-19] |> Some
           | h when h >= 164 && h <= 173 -> male.[1].[imc-19] |> Some
           | h when h >= 174 && h <= 183 -> male.[2].[imc-19] |> Some
           | h when h >= 184 && h <= 193 -> male.[3].[imc-19] |> Some
           | _ -> None

    | _ -> None