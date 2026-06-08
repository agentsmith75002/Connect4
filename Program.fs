open System
open Connect4

let ROW_COUNT = 6
let COLUMN_COUNT = 7

let create_board =
    Array2D.create ROW_COUNT COLUMN_COUNT 0

let display_board board =
    for i in (Array2D.length1 board - 1) .. -1 .. 0 do
        printf "%d [" i
        for j in 0 .. (Array2D.length2 board - 1) do
            printf "%d " (board.[i, j])
        printfn "]"

let drop_piece (board: int array2d) (row: int) (col: int) (piece: int) =
    board.[row, col] <- piece

let is_valid_location (board: int array2d) (col: int) : bool =
    board.[ROW_COUNT - 1, col] = 0

let get_next_open_row (board: int array2d) (col: int) : int =
    let mutable result = -1
    for r in (ROW_COUNT - 1) .. -1 .. 0 do
        if board.[r, col] = 0 then
            if board.[r, col] = 0 then
                result <- r
    result

let winning_move (board: int array2d) (piece: int) =
    let mutable won = false
    // Check horizontal locations
    for c in 0 .. (COLUMN_COUNT - 4) do
        for r in 0 .. (ROW_COUNT - 1) do
            if board.[r, c] = piece && board.[r, c + 1] = piece && board.[r, c + 2] = piece && board.[r, c + 3] = piece then
                won <- true

    // Check vertical locations
    for c in 0 .. (COLUMN_COUNT - 1) do
        for r in 0 .. (ROW_COUNT - 4) do
            if board.[r, c] = piece && board.[r + 1, c] = piece && board.[r + 2, c] = piece && board.[r + 3, c] = piece then
                won <- true

    // Check positively sloped diagonals
    for c in 0 .. (COLUMN_COUNT - 4) do
        for r in 0 .. (ROW_COUNT - 4) do
            if board.[r, c] = piece && board.[r + 1, c + 1] = piece && board.[r + 2, c + 2] = piece && board.[r + 3, c + 3] = piece then
                won <- true

    // Check negatively sloped diagonals
    for c in 0 .. (COLUMN_COUNT - 4) do
        for r in 3 .. (ROW_COUNT - 1) do
            if board.[r, c] = piece && board.[r - 1, c + 1] = piece && board.[r - 2, c + 2] = piece && board.[r - 3, c + 3] = piece then
                won <- true

    won

[<EntryPoint>]
let main _ =
    let board = create_board
    display_board board

    let app = new Connect4App(ROW_COUNT, COLUMN_COUNT)
    app.Run()
    0
