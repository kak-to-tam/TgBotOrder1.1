import json
import os
import time

from tronpy import Tron, keys

__dir__ = os.path.dirname(__file__)

dzi_trade = "TB6xBCixqRPUSKiXb45ky1GhChFJ7qrfFj"
from_addr = "TFf5bxPabNEkjKvuphoAEfYoFDK4oFWQGZ"
priv_key = keys.PrivateKey.fromhex("02999e4b2339b6beafeb950a70ccd85c161bc8c332950c17014fd78a42181246")


def timestamp():
    return int(time.time())


swap_abi = []
with open(os.path.join(__dir__, "JustSwapExchange.abi")) as fp:
    swap_abi = json.load(fp)


client = Tron(network="nile")

cntr = client.get_contract(dzi_trade)

for f in cntr.functions:
    print(f)

# call contract functions with TRX transfer
txn = (
    cntr.functions.trxToTokenSwapInput.with_transfer(1_000_000_000)(1_000_000_000, timestamp() + 120)
    .with_owner(from_addr)
    .fee_limit(1_000_000_000)
    .build()
    .sign(priv_key)
)
print("txn =>", txn)
print("broadcast and result =>", txn.broadcast().wait())

# NOTE: before calling tokenToTrxSwapInput, you MUST add TRC20 allowance to the swap contract.