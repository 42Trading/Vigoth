__author__ = 'saeedamen' # Saeed Amen / saeed@thalesians.com

#
# Copyright 2015 Thalesians Ltd. - http//www.thalesians.com / @thalesians
#
# Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the
# License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an
# "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
#
# See the License for the specific language governing permissions and limitations under the License.
#

"""
OptionsBBG

Sets properties for downloading data from Bloomberg.

"""

from pythalesians.util.loggermanager import LoggerManager
from datetime import datetime

class OptionsBBG:

    # TODO create properties that cannot be manipulated
    def __init__(self):
        self.logger = LoggerManager().getLogger(__name__)
