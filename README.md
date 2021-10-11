# Kata16: Business Rules

Imagine you’re writing an order processing application for a large company. In the past, this company used a fairly random mixture of manual and ad-hoc automated business practices to handle orders; they now want to put all these various ways of hanadling orders together into one whole: your application. After a full day of workshops you have gathered the following set of rules which need to be managed by the new system.

* If the payment is for a physical product, generate a packing slip for shipping.
* If the payment is for a book, create a duplicate packing slip for the royalty department.
* If the payment is for a membership, activate that membership.
* If the payment is an upgrade to a membership, apply the upgrade.
* If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
* If the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip (the result of a court decision in 1997).
* If the payment is for a physical product or a book, generate a commission payment to the agent.

Design a new system which can handle these rules and yet open to extension to new rules

(See also: [codekata.com](http://codekata.com/kata/kata16-business-rules/))

## Step 1

The easiest thing to do is to start at the beginning, and in this case that means the rule "If the payment is for a physical product, generate a packing slip for shipping.". Three things to be represented have been identified in this statement, the (completed?) payment, a product, a packing slip. Additionally another service/system/person has been identified - shipping.

### Assumptions

* The payment has been completed successfully
* The product and packing slip represent data to be transported
* The shipping department/application can be represented by a class for the purposes of this kata

### Choices

* The product (or potentially products) purchased will be included in an order, this is now represented by the order object
* Sub-systems will be stubbed out using Moq to ensure that the expected calls are being made

## Step 2

We've taken what can be the hardest step and started. Now we need to push forward, and implement the next rule "If the payment is for a book, create a duplicate packing slip for the royalty department.". Immediately a question is thrown up, is a Book a Physical product? Given that the requirements have come from the business, and they don't seem to be using arcane terminology it would be fair to assume that when we are dealing with a Book we are indeed also dealing with a Physical Product.

The next step to getting both tests to pass is to put a simple discriminator in the processor, so that the Royalty Department is only called when the order is for a book.

## Step 3

The next rule "If the payment is for a membership, activate that membership." introduces the concept of a Membership. This does not seem to be a PhysicalProduct (or derivation thereof), but an entirely new product type. This can be resolved by changing the relationship from:

![Product class relationship](/media/product-model-1.png)

To a diagram that looks like the following:

![New product class relationship](/media/product-model-2.png)

The activation of the membership will be represented on a new interface `IMemberServices`.

## Refactor time!

At this point the [`PostPaymentProcessor.Process`](https://github.com/steve-codemunkies/business-rules/blob/c784a9ad8be6371b25bf8234efba489b4ad51519/src/BusinessRules/PostPaymentProcessor.cs#L19-L35) is a _bit of a mess_. And this will not do. It's time to refactor.

We now have three rules implemented, two of the rules run in a specific (but different) situation, while the other rule runs in two situations. At this point this looks like something we could implement with the [strategy pattern](https://www.oodesign.com/strategy-pattern.html). We'll do this by initially introducing a new `IRuleStrategy` interface, then re-implementing (with tests) the existing rules. The final step is to modify `PostPaymentProcessor.Process` to use the new strategies.