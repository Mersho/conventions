#!/usr/bin/env bash
set -euxo pipefail

# cd to directory of this script
cd "$(dirname "$0")"
npm install --verbose
npm install --verbose @commitlint/cli conventional-changelog-conventionalcommits
npx commitlint --version
npx commitlint $@
