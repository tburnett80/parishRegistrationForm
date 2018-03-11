docker run -it -v dbdata:/_data -v /tmp/backups:/backup alpine \
   tar -cjf /backup/dbdata_"$(date '+%y-%m-%d')".tar.bz2 /_data