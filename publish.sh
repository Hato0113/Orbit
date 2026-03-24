#!/bin/bash
set -e

RID="${1:-osx-arm64}"
OUTPUT_DIR="publish"

dotnet publish -c Release -r "$RID" --self-contained -p:PublishSingleFile=true -o "$OUTPUT_DIR"

echo ""
echo "Build complete: $OUTPUT_DIR/obt"
