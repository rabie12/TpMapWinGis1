   PID USER      PR  NI    VIRT    RES    SHR S  %CPU  %MEM     TIME+ COMMAND
2982109 busines+  20   0 6829160  44304  20972 S   3.3   0.3   0:00.10 java 

i want to check rugular the status of this app but i have this issue :

systemctl status busines+
Invalid unit name "busines+" was escaped as "busines\x2b" (maybe you should use systemd-escape?)
Unit busines\x2b.service could not be found.
