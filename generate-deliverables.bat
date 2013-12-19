@echo off

set root=%~dp0
set docs=%root%docs
set scripts=%root%scripts
set deliverables=%root%deliverables

::
:: Convert every .doc[x] in docs folder to .pdf
:: 
for %%F in (%docs%\*.doc*) do powershell.exe -file %scripts%\doc2pdf.ps1 "%%~dpnxF" "%deliverables%\%%~nF.pdf"
