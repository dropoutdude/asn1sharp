#!/bin/bash

branch=$1;
build=$2;
key=$3;
tag=$4;

if [ $branch == "master" ] || [ -n "$tag" ]; then
  version=$(<Version.txt);      
  if [ -n "$tag" ] && ! [[ $tag == v[0-9].[0-9].[0-9] && $tag == v${version} ]]; then
    echo "Tag $tag has wrong format - exiting! Expected v$version"
    exit 1;
  fi
  if [ $branch == "master" ]; then      
    version=$(printf "%s-p%s" $version "$build");
    printf "Prerelease Version equals %s\n" $version;
  fi
  printf "Version equals %s\n" $version;
  dotnet pack -c Release -p:PackageVersion="$version";
  echo "./asn1sharp/bin/Release/asn1sharp.${version}.nupkg"
  dotnet nuget push "./asn1sharp/bin/Release/asn1sharp.${version}.nupkg" -k $key -s https://api.nuget.org/v3/index.json
fi