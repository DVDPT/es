param([string]$doc,[string]$pdf)

$doc
$pdf

$word_app = New-Object -ComObject Word.Application
$document = $word_app.Documents.Open($doc)
$document.SaveAs( $pdf, 17)
$document.Close()
$word_app.Quit()