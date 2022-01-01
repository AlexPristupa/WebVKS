#!/bin/bash
cp ./mentolvks.service /etc/systemd/system/
systemctl daemon-reload
systemctl enable mentolvks.service
systemctl start mentolvks
