// This is a comment because it follows a double slash (`//`).

// Here are three separate lists of items that can be referenced in an example sentence. You can have up to 10 of these.
@ list food =
- pizza
- burger
- ice cream
- soda

@ list pet =
- cat
- dog
- fish

@ list sports =
- soccer
- tennis
- cricket
- basketball
- baseball
- football

// List of phonetic pronunciations
@ speech:phoneticlexicon
- cat/k ae t
- fish/f ih sh

// Here are two sections of training sentences. 
#TrainingSentences_Section1
- you can include sentences without a class reference
- what {@pet} do you have
- I like eating {@food} and playing {@sports}
- my {@pet} likes {@food}

#TrainingSentences_Section2
- you can include more sentences without a class reference
- or more sentences that have a class reference like {@pet}