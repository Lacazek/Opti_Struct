# Opti_Struct

The aim of this code is to provide a solution to automaticaly create optimisation structure. According to txt file, users can decide to create his own structure according to the following vocabulary:

exemple : PTV - ITV : PTV - ITV

: --> on the left side; this is the name of the structure

: --> on the right side ; this the operation

PTV --> this the first structure

- --> this the operator
- 
ITV --> this the second structure

There are 5 operator :

"|" for the sum of the structure

"&" for the intersection

"-" for the soustraction between two structures

"^" for the union

+ for create margin to defined structure
    For using this operator; the right side corresponds to margin in mm (exemple : PTV : ITV + 5.00)
