

# OpenRealEstate.NET Validation

This library allows us to validate the OpenRealEstate `Listing` instances. There's also a few flavours of validation: strict or just some basic validation.

[![Build status](https://ci.appveyor.com/api/projects/status/x3x934tkrks7di9l/branch/master?svg=true)](https://ci.appveyor.com/project/PureKrome/openrealestate-net-validation) [![NuGet](https://img.shields.io/nuget/v/OpenRealEstate.NET.Validation.svg)](https://www.nuget.org/packages/OpenRealEstate.NET.Validation) [![NuGet](https://img.shields.io/nuget/dt/OpenRealEstate.NET.Validation.svg)](https://www.nuget.org/packages/OpenRealEstate.NET.Validation) [![MyGet Pre Release](https://img.shields.io/myget/openrealestate-net/vpre/OpenRealEstate.NET.Validation.svg)]()

---

## Rule sets
Rulesets are options that define how many of the Listing properties are checked.

| RuleSet   | Description |
|-----------|-------------|
| `Default` | The default ruleset. This is the most basic form of validation and offers the smallest set of properties to check. |
| `Normal`  | This is the ruleset which is recommended for most scenario's. Technically, it's `default, Normal`. |
| `Strict`  | This final ruleset is the most strictest for validation checks. Tehnically, it's `default, Normal, Strict`. | 

e.g.
```
// Validate a listing against the strictest validation rule.
var validator = new ResidentialListingValidator();
var listing = GetListing<ResidentialListing>();
var result = validator.Validate(listing, 
                                ruleSet: ResidentialListingValidator.StrictRuleSet);
```
---

## Contributing

Discussions and pull requests are encouraged :) Please ask all general questions in this repo or pick a specialized repo for specific, targetted issues. We also have a [contributing](https://github.com/OpenRealEstate/OpenRealEstate/blob/master/CONTRIBUTING.md) document which goes into detail about how to do this.

## Code of Conduct
Yep, we also have a [code of conduct](https://github.com/OpenRealEstate/OpenRealEstate/blob/master/CODE_OF_CONDUCT.md) which applies to all repositories in the OpenRealEstate organisation.

## Feedback
Yep, refer to the [contributing page](https://github.com/OpenRealEstate/OpenRealEstate/blob/master/CONTRIBUTING.md) about how best to give feedback - either good or needs-improvement :)

---
