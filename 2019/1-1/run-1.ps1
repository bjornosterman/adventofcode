cat .\input.txt | %{ [math]::floor($_ / 3)-2 } | measure -sum