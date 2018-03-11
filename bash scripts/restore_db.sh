docker run -it -v dbdata:/_data -v /tmp/backups:/backup alpine \
   sh -c "rm -rf /_data/* /_data/..?* /_data/.[!.]* ; tar -C /_data/ -xjf /backup/dbdata_18-03-11.tar.bz2"